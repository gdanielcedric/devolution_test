# devolution_test  

Outils :  
keycloak :24.0.1  
postgres 15  
Entity Framework  
Github  
Visual Studio  
Docker

# I. Architecture

-	Keycloak et sa BD  
-	BD projet  
-	Api interconnectée avec Keycloak et la BD du projet  

# II. Recupération du projet et configuraiton  

Saisir les commandes git suivantes pour récupérer les projets api et front :  
- gh repo clone gdanielcedric/devolution_test_api  
Ou les télécharger via les liens suivants :  
https://github.com/gdanielcedric/devolution_test_api.git  
Une fois récupérer nous allons lancer les contenaires dockers pour accéder aux services keycloak et BD  
A- Keycloak  
1- Ouvrir le dossier : cd /keycloak  
2- Saisir : docker-compose up -d  
B- BD  
1- Ouvrir le dossier : cd /bd_api  
2- Saisir : docker-compose up -d (s'assurer que le port 5433 est libre, au besoin vous pouvez le modifier dans le docker-compose.yml)  
C- Api  
1- Ouvrir le dossier : cd /api/api  
2- Saisir : dotnet run (ou cliquer sur l'icone de démarrage dans visual studio)  

# III. Configuration de keycloack

Lorsque tous les projets ont été téléchargé et bien configuré, l'instance de Keycloak est accessible via :  
url_keycloak : http://localhost:8080/admin  
username : admin  
password : admin  
1- Realm :  
Le realm est une sorte de conteneur dédié à une application au sein de Keycloak, celui qui créé pour ce projet est : auth-api.  
2- Le client :  
Le client est le canal de connexion, configurable dans le realm, il y'en a de plusieurs types, web, api...  
3- Les roles :  
Les roles à utiliser dans l'application  
4- Les utilisateurs (users) :  
Les utilisateurs de l'application  
Keycloack permet la gestion des roles, des utilisateurs, le type d'authentification, simple ou MFA...  

Utilisateurs créés pour test  
gdanielcedric : P@ssword2025! (Admin)  
amazon : P@ssword2025! (Amazon)  

# IV. Configuration de la base de données

J’ai monté une image de postgres 15 dans docker, à laquelle je me suis connecté pour créer une base de données vide ; 
Ensuite pour la migration de la base de données, j’ai utilisé entity framework, avec les commandes suivantes :  
-	Dotnet ef migrations add « nom_migration » (pour créer un fichier de migration)  
-	Dotnet ef database update (pour mettre à jour la base de données)  

# V. API
b-	Auth  
Contient une classe statique UserRoles qui défini les différents roles ;  
c-	Contexts  
Contient ApiDbContext: DbContext, le context de la base de données du projet, avec les tables à implémenter, le schema à utiliser et les différentes clés ;  
d-	Controllers  
Tous les contrôleurs du projet y sont, notamment le contrôleur « simulation » et « subscriptions »   
e-	DTO  
Dossier des objets utilisés en paramètres des contrôleurs et autres services  
f-	Enums  
2 fichier enums pour la liste des garanties et le type de souscriptions  
g-	Helpers  
Contient un fichier Utility qui implémente les fonctions réutilisables  
h-	Interfaces  
Le dossier des interfaces  
i-	Migrations  
Le dossier créé après avoir effectué la commande : Dotnet ef migrations add « nom_migration »  
j-	Models  
Contient tous les modèles, classes associées aux tables de notre base de données  
k-	Services  
Contient les services, qui implémentent les interfaces  

# VI. Fonctionnalités de base  
1- CalculatePrime  
Pour le calcul des primes, j'ai créé une classe abstraite Guaranty qui implémente une méthode abstraite CalculatePrime ;  
Pour chaque type de Garantie, j'ai créé une classe héritant de Guaranty et redefini pour chacune d'elle la méthode CalculatePrime selon les spécificités definies;  
Exemple :  
public class PlafondTierceGuaranty : Guaranty  
{  
    public double Value { get; set; }  
    public override double CalculatePrime()  
    {  
        // valeur assurée  
        double assur_value = Value * 0.5;  
        // valeur de la prime  
        double prime = assur_value * 4.2 / 100;  
        if (prime < 100000) prime = 100000;  
        return prime;  
    }  
}  
Ensuite, j'ai créé une méthode CalculatePrime dans un fichier Utility, de sorte à ce que ce soit réutilisable, laquelle méthode va, en fonction du véhicule et des garanties passées en paramètre retourné le montant total de la prime :  
public static double CalculatePrime(Vehicle vehicle, List<string> garanties)  
{  
    double amount = 0;  

    // check if garanties contain RC and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.RC.Humanize()))  
    {  
        var vg = new RCGuaranty  
        {  
            FiscalPower = vehicle.FiscalPower,  
        };  

        amount += vg.CalculatePrime();  
    }  

    // check if garanties contain TIERCE_COLLISION and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.TIERCE_COLLISION.Humanize()))  
    {  
        var vg = new CollisionTierceGuaranty  
        {  
            Value = vehicle.ValueNeuve,  
        };  

        amount += vg.CalculatePrime();  
    }  

    // check if garanties contain DAMAGES and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.DAMAGES.Humanize()))  
    {  
        var vg = new DamageGuaranty  
        {  
            Value = vehicle.ValueNeuve,  
        };  

        amount += vg.CalculatePrime();  
    }  

    // check if garanties contain TIERCE PLAFONNEE and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.TIERCE_PLAFONNEE.Humanize()))  
    {  
        var vg = new PlafondTierceGuaranty  
        {  
            Value = vehicle.ValueVenale,  
        };  

        amount += vg.CalculatePrime();  
    }  

    // check if garanties contain VOL and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.VOL.Humanize()))  
    {  
        var vg = new VolGuaranty  
        {  
            Value = vehicle.ValueVenale,  
        };  

        amount += vg.CalculatePrime();  
    }  

    // check if garanties contain INCENDIE and calculate the appropriate prime to add global amount  
    if (garanties.Contains(GuaranTypeEnum.INCENDIE.Humanize()))  
    {  
        var vg = new IncendieGuaranty  
        {  
            Value = vehicle.ValueVenale,  
        };  

        amount += vg.CalculatePrime();  
    }  

    return amount;  
}  
2- Les listes de retour  
Les apis qui retournent des listes spécifiques (souscripteurs, souscriptions, simulations) vérifient le type d'utilisateur et en fonction retourne, si Amazon, une liste triée pour lui, si Admin, tous les éléments :  
Exemple :  
[HttpPost("all")]  
public Task<IActionResult> GetSimulations(SearchEntityDto<SimulationFilter> search)  
{  
    string? idConnected = "";  

    var isAdmin = User.IsInRole("Admin");  

    if (!isAdmin) idConnected = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

    // get all simulations  
    var all = _simulationsServices.getAll(search, idConnected);  

    return Task.FromResult<IActionResult>(Ok(all));  
}  
﻿namespace api.DTO
{
    public class AssurProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Garanties { get; set; } = new List<string>();
        public List<string> Categories { get; set; } = new List<string>();
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}

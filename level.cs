using System.Xml.Serialization;

namespace L3_Survival;

[XmlRoot("Niveaux")]
public class ListeNiveaux
{
    [XmlElement("Niveau")]
    public Level[] Niveaux { get; set; }
}

public class Level
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    public string Type { get; set; }
    public int VitesseDefilement { get; set; }
}
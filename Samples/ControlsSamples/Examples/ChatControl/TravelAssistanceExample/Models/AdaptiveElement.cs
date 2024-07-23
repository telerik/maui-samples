using System.Text.Json.Serialization;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AdaptiveColumnSet), typeDiscriminator: "ColumnSet")]
[JsonDerivedType(typeof(AdaptiveTextBlock), typeDiscriminator: "TextBlock")]
[JsonDerivedType(typeof(AdaptiveImage), typeDiscriminator: "Image")]
public class AdaptiveElement
{
    public bool Separator { get; set; }
    public string Speak { get; set; }
    public string Type { get; set; }
}
using Microsoft.Maui.Devices;
using System;

namespace QSF.Examples.CarouselControl.FirstLookExample;

public class Dish
{
    public string Category { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public double Rating { get; set; }

    public double FilledStars => Math.Floor(this.Rating);

    public string SelectedRatingMarks => new string(this.RatingMark, this.RatingMarksCount);

    public string UnselectedRatingMarks => new string(this.RatingMark, 5 - this.RatingMarksCount);

    public string Description { get; set; }

    public string Image { get; set; }

    private int RatingMarksCount => Math.Clamp((int)this.FilledStars, 0, 5);

    private char RatingMark => DeviceInfo.Idiom == DeviceIdiom.Phone ? '\u25CF' : '\u2605';
}

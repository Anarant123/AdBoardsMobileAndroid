using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Xamarin.Forms;

#nullable enable

namespace AdBoardsMobileAndroid.Models.db
{
    public partial class Ad
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("price")]
        public int? Price { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("cotegorysId")]
        public int? CotegorysId { get; set; }

        [JsonPropertyName("personId")]
        public int? PersonId { get; set; }

        [JsonPropertyName("typeOfAdId")]
        public int? TypeOfAdId { get; set; }

        [JsonPropertyName("complaints")]
        public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        [JsonPropertyName("cotegorys")]
        public virtual Category? Cotegorys { get; set; }

        [JsonPropertyName("favorites")]
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        [JsonPropertyName("person")]
        public virtual Person? Person { get; set; }

        [JsonPropertyName("typeOfAd")]
        public virtual TypeOfAd? TypeOfAd { get; set; }

        public ImageSource Img
        {
            get
            {
                return ImageSource.FromStream(() =>
                {
                    return new MemoryStream(this.Photo);
                });
            }
        }

        public string getType
        {
            get
            {
                if (this.TypeOfAdId == 1)
                    return "Куплю";
                else
                    return "Продам";
            }
        }
        public string getPrice
        {
            get
            {
                return $"{this.Price} ₽";
            }
        }
    }
}

#nullable restore

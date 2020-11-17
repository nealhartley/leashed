using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace leashApi.Models
{
    public class PictureDogJoin
    {
       public int PictureId { get; set; }
       public Picture Picture { get; set; }
       public int DogId { get; set; }
       public Dog Dog { get; set; }
    }
}
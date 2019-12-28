using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieGallery.Models.ViewModels
{
    public class SearchActorViewModel<T> where T : ActorResultItem
    {
        public ActorResultItem Actor { get; set; }
        public SearchActorViewModel(ActorResultItem actor)
        {
            Actor = actor;
        }
        
    }
}
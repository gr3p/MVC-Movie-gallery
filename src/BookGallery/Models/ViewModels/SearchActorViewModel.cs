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
            Actor.results = actor.results.OrderByDescending(x => !string.IsNullOrEmpty(x.profile_path))
                .ThenByDescending(y => y.popularity)
                .ToArray();
        }
        
    }
}
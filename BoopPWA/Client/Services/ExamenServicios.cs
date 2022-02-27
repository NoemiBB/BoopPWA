using Microsoft.AspNetCore.Components;
using Radzen;

namespace BoopPWA.Client.Services
{
    public class ExamenServicios
    {
        private readonly NavigationManager navigation;
        public ExamenServicios(NavigationManager nav)
        {
            navigation = nav;
        }
        public void Export(string table, string type, Query query = null)
        {
            navigation.NavigateTo(query != null ? query.ToUrl($"/export/examen/{table}/{type}") : $"/export/examen/{table}/{type}", true);
        }
    }
}

using BrowserGame.BusinessLayer.Effects;
using BrowserGame.BusinessLayer.Resources;
using BrowserGame.BusinessLayer.Villages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer
{
	public static class BusinessLayerServiceCollectionExtensions
	{
		public static IServiceCollection AddGameServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddScoped<IGameResourceService, GameResourceService>();
			services.AddScoped<IBuildingService, BuildingService>();
			services.AddScoped<IVillageService, VillageService>();
			services.AddScoped<IEffectService, EffectService>();

			return services;
		}
	}
}

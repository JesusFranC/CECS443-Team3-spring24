using Team3.ThePollProject.ConfigService.ConfigModels;

namespace Team3.ThePollProject.ConfigService;

public interface IConfigServiceJson
{
    RideAlongConfigModel GetConfig();
}

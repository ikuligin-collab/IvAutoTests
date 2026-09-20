using apitest.DTO.PetModelsDTO;
using Refit;

namespace apitest.Interfaces.PetStore;

public interface IPetApi
{
    [Get("/pets")] 
    Task<AllPetsResponseDTO> GetAllPetsAsync();
    
    [Get ("/pets/{Id}")]
    Task<PetDTO> GetPetByIdAsync(string id);
    
    [Get("/pets")] 
    Task<AllPetsResponseDTO> GetAllPetsByStatusAndLimitAsync([Query] string status, [Query] int limit);
}
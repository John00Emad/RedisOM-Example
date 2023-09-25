using Microsoft.AspNetCore.Mvc;
using RedisOM.DTOs;
using RedisOM.Models;
using RedisOM.Repositories;

namespace RedisOM.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TripLocationsController : ControllerBase
{
    private readonly TripLocationsRepository _tripLocationsRepository;

    public TripLocationsController(TripLocationsRepository tripLocationsRepository)
    {
        _tripLocationsRepository = tripLocationsRepository;
    }

    [HttpPost("AddBulkOfTripLocations")]
    public async Task AddBulkOfUsers(AddBulkOfTripLocationsDto bulkOfTripLocationsDto)
    {
        await _tripLocationsRepository.AddBulkOfTripLocations(bulkOfTripLocationsDto);
    }
    
    [HttpPost("GetNearestTrip")]
    public async Task<IList<TripLocation>> AddBulkOfUsers(GetNearestTripDto getNearestTripDto)
    {
        return await _tripLocationsRepository.GetNearestTrip(getNearestTripDto);
    }
}
using Microsoft.AspNetCore.Mvc;
using RedisOM.DTOs;
using RedisOM.Models;
using RedisOM.Repositories;
using StackExchange.Redis;

namespace RedisOM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetAllUsers")]
    public async Task<IList<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    [HttpPost("GetNearestUsers")]
    public async Task<IList<User>> GetNearestUsers(GetNearestUsersDto getNearestUsersDto)
    {
        return await _userRepository.GetNearestUesrs(getNearestUsersDto.Location, getNearestUsersDto.Radius,
            getNearestUsersDto.DistanceUnit);
    }

    [HttpPost("GetNearestUsersWithNative")]
    public async Task<IList<GetNearestUsersResponseDto>> GetNearestUsersWithNative(
        GetNearestUsersDto getNearestUsersDto)
    {
        var result = await _userRepository.GetNearestUesrsWithNative(getNearestUsersDto.Location,
            getNearestUsersDto.Radius, getNearestUsersDto.DistanceUnit);

        return result;
    }

    [HttpPost("AddUser")]
    public async Task<string> AddUser(AddUserDto userDTo)
    {
        return await _userRepository.AddUser(userDTo.Name, userDTo.CurrentLocation);
    }

    [HttpPost("AddBulkOfUsers")]
    public async Task AddBulkOfUsers(AddBulkOfUsersDto addBulkOfUsersDto)
    {
        await _userRepository.AddBulkOfUsers(addBulkOfUsersDto);
    }
}
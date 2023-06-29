using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace VideoPlatform.Api.Controllers;

/// <summary>
///     ValuesController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    /// <summary>
    ///     GET api/values
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new[] { "value1", "value2" };
    }

    /// <summary>
    ///     GET api/values/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public ActionResult<string> Get(int id)
    {
        return "value";
    }

    /// <summary>
    ///     POST api/values
    /// </summary>
    /// <param name="value"></param>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    /// <summary>
    ///     api/values/5
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    [HttpPut("{id:int}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    /// <summary>
    ///     DELETE api/values/5
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    public void Delete(int id)
    {
    }
}
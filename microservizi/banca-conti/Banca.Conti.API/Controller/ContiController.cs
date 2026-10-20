using Banca.Conti.Business;
using Banca.Conti.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Conti.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContiController : ControllerBase
{
    private readonly IContiService _contiService;

    public ContiController(IContiService contiService)
    {
        _contiService = contiService;
    }

    /// <summary>
    /// Restituisce l'elenco completo dei conti correnti.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var conti = await _contiService.GetAllAsync();
        return Ok(conti);
    }

    /// <summary>
    /// Recupera un conto corrente tramite IBAN.
    /// </summary>
    [HttpGet("{iban}")]
    [ProducesResponseType(typeof(ContoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIban(string iban)
    {
        var conto = await _contiService.GetByIbanAsync(iban);
        if (conto == null)
        {
            return NotFound(new { message = $"Nessun conto trovato con IBAN {iban}" });
        }

        return Ok(conto);
    }

    /// <summary>
    /// Crea e censisce un nuovo conto corrente.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ContoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ContoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var nuovoConto = await _contiService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIban), new { iban = nuovoConto.Iban }, nuovoConto);
        }
        catch (InvalidOperationException ex)
        {
            // Gestione vincoli di business (es. IBAN duplicato)
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Aggiorna i saldi o i dati anagrafici del conto.
    /// </summary>
    [HttpPut("{iban}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string iban, [FromBody] ContoDto dto)
    {
        if (iban != dto.Iban)
        {
            return BadRequest(new { message = "L'IBAN indicato nell'URL non coincide con il corpo della richiesta." });
        }

        var aggiornato = await _contiService.UpdateAsync(dto);
        if (!aggiornato)
        {
            return NotFound(new { message = $"Conto con IBAN {iban} non trovato per l'aggiornamento." });
        }

        return NoContent();
    }
}
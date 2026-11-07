namespace DemoAPIPOO2026_2.Estrategias;
public interface IConsolidacionFactory { IConsolidacionStrategy Crear(string tipo); }
public class ConsolidacionFactory : IConsolidacionFactory
{
    public IConsolidacionStrategy Crear(string tipo)
    {
        if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("El tipo es obligatorio.");
        if (tipo.Trim().Equals("Urbano", StringComparison.OrdinalIgnoreCase)) return new ConsolidacionUrbanaStrategy();
        if (tipo.Trim().Equals("Rural", StringComparison.OrdinalIgnoreCase)) return new ConsolidacionRuralStrategy();
        throw new ArgumentException("El tipo debe ser Urbano o Rural.");
    }
}

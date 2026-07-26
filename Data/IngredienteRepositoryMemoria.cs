using Domain.Model;

namespace Data
{
    public class IngredienteRepositoryMemoria : IIngredienteRepository
    {
        private readonly List<Ingrediente> _ingredientes = new();
        private readonly object _candado = new();
        private int _siguienteId = 1;

        public IngredienteRepositoryMemoria()
        {
            SembrarDatos();
        }

        private void SembrarDatos()
        {
            Agregar(new Ingrediente(0, "Harina 000", "Harina de trigo", "kg", 200, 50));
            Agregar(new Ingrediente(0, "Manteca", "Manteca sin sal", "kg", 40, 20));
            Agregar(new Ingrediente(0, "Azúcar", "Azúcar refinada", "kg", 15, 30));
            Agregar(new Ingrediente(0, "Huevos", "Huevos de gallina", "docena", 25, 10));
        }

        private void Agregar(Ingrediente ingrediente)
        {
            ingrediente.SetId(_siguienteId++);
            _ingredientes.Add(ingrediente);
        }

        public Task<Ingrediente?> GetAsync(int id)
        {
            lock (_candado)
                return Task.FromResult(_ingredientes.FirstOrDefault(i => i.Id == id));
        }

        public Task<IEnumerable<Ingrediente>> GetAllAsync()
        {
            lock (_candado)
                return Task.FromResult(_ingredientes.ToList().AsEnumerable());
        }

        public Task AddAsync(Ingrediente ingrediente)
        {
            lock (_candado)
                Agregar(ingrediente);
            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(Ingrediente ingrediente)
        {
            lock (_candado)
            {
                var indice = _ingredientes.FindIndex(i => i.Id == ingrediente.Id);
                if (indice < 0) return Task.FromResult(false);
                _ingredientes[indice] = ingrediente;
                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_candado)
            {
                var existente = _ingredientes.FirstOrDefault(i => i.Id == id);
                if (existente == null) return Task.FromResult(false);
                _ingredientes.Remove(existente);
                return Task.FromResult(true);
            }
        }
    }
}

using DAOSQL.BO;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAOSQL
{
    public class DAOSQL : DbContext, Interfaces.IDAO
    {
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Shoe> Shoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlite(@"Filename=N:\Programing\Wizualne\CarApp2\DAOEF\shoesdb8.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Shoe>()
                .HasOne(s => s.Producer)
                .WithMany(p => p.Shoes)
                .HasForeignKey(s => s.ProducerId)
                .IsRequired();
        }
        public void AddShoe(IShoe shoe)
        {
            BO.Shoe s = shoe as BO.Shoe;
            Shoes.Add(s);
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            Producers.Add(p);
        }

        public IShoe CreateNewShoe()
        {
            return new BO.Shoe();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<IShoe> GetAllShoes()
        {
            var shoes = Shoes.Include("Producer").ToList();
            return shoes;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            using (var context = new DAOSQL())
            {
                context.Database.EnsureCreated();
            }

            return Producers.ToList();
        }

        public void RemoveShoe(IShoe shoe)
        {
            //throw new NotImplementedException();
            Shoes.Remove(shoe as BO.Shoe);
        }

        public void RemoveProducer(IProducer producer)
        {
            //throw new NotImplementedException();
            Producers.Remove(producer as BO.Producer);
        }

        public void UpdateShoe(IShoe shoe)
        {
            if (shoe is Shoe s)
            {
                var entity = Shoes.Find(s.Id);
                if (entity != null)
                {
                    Entry(entity).CurrentValues.SetValues(s);
                }
            }
        }

        public void UpdateProducer(IProducer producer)
        {
            if (producer is Producer p)
            {
                var entity = Producers.Find(p.Id);
                if (entity != null)
                {
                    Entry(entity).CurrentValues.SetValues(p);
                }
            }

            this.SaveChanges();
        }

        void IDAO.SaveChanges()
        {
            this.SaveChanges();
        }

        public void UndoChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.State = EntityState.Unchanged;
                }
            }
            this.SaveChanges();
        }
    }
}

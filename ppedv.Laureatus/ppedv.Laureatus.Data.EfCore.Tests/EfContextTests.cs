using ppedv.Laureatus.Model;
using System;
using Xunit;

namespace ppedv.Laureatus.Data.EfCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void Can_create_db()
        {
            using var con = new EfContext();

            con.Database.EnsureDeleted();

            Assert.True(con.Database.EnsureCreated());
        }

        [Fact]
        public void Is_created_and_modified_set_on_savechange()
        {
            var p = new Person() { Name = "Alf" };

            using (var con = new EfContext())
            {
                con.Add(p);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check created
                var loaded = con.Persons.Find(p.Id);
                Assert.NotEqual(DateTime.MinValue, loaded.Created);
                Assert.NotEqual(DateTime.MinValue, loaded.Modified);
                Assert.Equal(loaded.Created, loaded.Modified);

                loaded.Name = "Willi";
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check modified
                var loaded = con.Persons.Find(p.Id);
                Assert.NotEqual(loaded.Created, loaded.Modified);
            }
        }


        [Fact]
        public void Can_CRUD_Person()
        {
            var p = new Person() { Name = $"Fred_{Guid.NewGuid()}" };
            var newName = $"Wilma_{Guid.NewGuid()}";

            //CREATE
            using (var con = new EfContext())
            {
                con.Add(p);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //READ
                var loaded = con.Persons?.Find(p.Id);
                Assert.Equal(p.Name, loaded?.Name);

                //UPDATE
                loaded.Name = newName;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check UPDATE
                var loaded = con.Persons?.Find(p.Id);
                Assert.Equal(newName, loaded?.Name);

                //DELETE
                con.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check DELETE
                var loaded = con.Persons.Find(p.Id);
                Assert.Null(loaded);
            }
        }
    }
}
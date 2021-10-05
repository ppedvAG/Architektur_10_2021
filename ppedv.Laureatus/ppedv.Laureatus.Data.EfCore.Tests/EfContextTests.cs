using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.Laureatus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                //Assert.NotEqual(DateTime.MinValue, loaded.Created);
                //Assert.NotEqual(DateTime.MinValue, loaded.Modified);
                //Assert.Equal(loaded.Created, loaded.Modified);
                loaded.Created.Should().NotBe(DateTime.MinValue);
                loaded.Modified.Should().NotBe(DateTime.MinValue);
                loaded.Modified.Should().Be(loaded.Modified);

                loaded.Name = "Willi";
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check modified
                var loaded = con.Persons.Find(p.Id);
                //Assert.NotEqual(loaded.Created, loaded.Modified);
                loaded.Modified.Should().NotBe(loaded.Created);
                loaded.Modified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(1000));
            }
        }

        [Fact]
        public void Can_create_and_read_Person()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var p = fix.Create<Person>();

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
                loaded.Should().BeEquivalentTo(p, c => c.IgnoringCyclicReferences());
            }
        }

        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;

            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
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
using System;
using System.Collections.Generic;
using System.Linq;
using Things.Utilities.Comparer.Generic;
using Xunit;

namespace GenericClassComparer.Tests
{
    public class Dummy
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class GenericClassComparerTest
    {
        public static IList<object> Dummies
        {
            get
            {
                return new object[]
                {
                   new { Id = 1, Name = "Nome1" },
                   new { Id = 2, Name = "Nome1" },
                   new { Id = 3, Name = "Nome3" },
                   new { Id = 4, Name = "Nome4" },
                   new { Id = 4, Name = "Nome4" },
                };
            }
        }

        public static List<Dummy> Dummies2 = new List<Dummy>
                {
                   new Dummy { Id = 1, Name = "Nome1" },
                   new Dummy { Id = 2, Name = "Nome1" },
                   new Dummy { Id = 3, Name = "Nome3" },
                   new Dummy { Id = 4, Name = "Nome4" },
                   new Dummy { Id = 4, Name = "Nome4" },
                };


        [Fact]
        public void PropNameShouldBeEqual()
        {
            var genericClassComparer = new GenericClassComparer<dynamic>("Name");

            // Assert
            Assert.True(genericClassComparer.Equals(Dummies[0], Dummies[1]));

        }

        [Fact]
        public void PropIdShouldBeDifferent()
        {
            var genericClassComparer = new GenericClassComparer<dynamic>("Id");

            // Assert
            Assert.False(genericClassComparer.Equals(Dummies[2], Dummies[3]));

        }


        [Fact]
        public void PropIdAndNameShouldBeEqual()
        {
            var genericClassComparer = new GenericClassComparer<dynamic>("Id", "Name");

            // Assert
            Assert.True(genericClassComparer.Equals(Dummies[3], Dummies[4]));

        }

        [Fact]
        public void QuantityElementsDistinctShouldBeFour()
        {
            var t = Dummies2.Distinct(new GenericClassComparer<Dummy>("Id")).ToList();

            // Assert
            Assert.Equal(4, t.Count);
        }


        [Fact]
        public void WithoutPropParamsAlwaysShouldBeEquals()
        {
            var genericClassComparer = new GenericClassComparer<dynamic>();

            // Assert
            Assert.True(genericClassComparer.Equals(Dummies[3], Dummies[4]));

        }

        [Fact]
        public void WhenPropNameDontExistThrowNullReferenceException()
        {
            var genericClassComparer = new GenericClassComparer<dynamic>("Gender");

            // Assert
            Assert.Throws<NullReferenceException>(() => genericClassComparer.Equals(Dummies[1], Dummies[2]));

        }
    }
}

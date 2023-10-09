﻿using Car_Data;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Car_Rental.Data.Classes
{
    public class CollectionData : IData
    {
        private Dictionary<Type, IEnumerable<object>> _data = new Dictionary<Type, IEnumerable<object>>();

        public CollectionData()
        {
            SeedData();
        }

        public void SeedData()
        {
            _data[typeof(IPerson)] = new List<IPerson>
            {
                new Customer("Svensson", "Bosse", 1811, 1),
                new Customer("Pettersson", "Pelle", 5152, 2),
                new Customer("Mört", "Maria", 7615, 3)
            };

            _data[typeof(IVehicle)] = new List<IVehicle>
            {
                new Car(200, 15, 4500, "KYP404", "Renault", true, VehicleTypes.Sedan),
                new Car(1000, 10, 15500, "ARL999", "Kia", true, VehicleTypes.Van),
                new Car(2500, 300, 2200, "GAP613", "Ferrari", true, VehicleTypes.Combi),
                new Motorcycle(450, 20, 1500, "JUS666", "Yamaha", true, VehicleTypes.Motorcycle)
            };

            _data[typeof(IBooking)] = new List<IBooking>();

            List<VehicleTypes> vehicleTypesList = new List<VehicleTypes>
            {
                VehicleTypes.Sedan,
                VehicleTypes.Combi,
                VehicleTypes.Van,
                VehicleTypes.Motorcycle
            };

            _data[typeof(VehicleTypes)] = vehicleTypesList.Cast<object>().ToList();


        }

        public List<T> Get<T>(Expression<Func<T, bool>>? expression = null)
        {
            Type targetType = typeof(T);

            if (_data.TryGetValue(targetType, out var targetList))
            {
                if (expression != null)
                {
                    var filter = expression.Compile();
                    return targetList.Cast<T>().Where(filter).ToList();
                }

                return targetList.Cast<T>().ToList();
            }

            throw new NotImplementedException();
               
        }


        public void Add<T>(T item)
        {
            Type targetType = typeof(T);

            if (_data.TryGetValue(targetType, out var targetList))
            {
                ((List<T>)_data[typeof(T)]).Add(item);

            }
            else
            {
                throw new NotImplementedException();
            }
           
        }

        public void AddCustomer(string lName, string fName, int SSN) { }

      
    }
}

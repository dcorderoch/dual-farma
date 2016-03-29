using System;
using System.Data;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.Test
{
    public class TestMain
    {
        static void Main(string[] args)
        {
            UserTesting.test();
            DoctorTesting.test();
            MedicineTesting.test();
            ClientTesting.test();
            PrescriptionTesting.test();
            OrderTesting.test();
        }
    }
}
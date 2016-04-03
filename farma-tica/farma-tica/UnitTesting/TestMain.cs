using System;
using System.Data;
using dual_farma.DAL.Models;

namespace dual_farma.UnitTesting
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
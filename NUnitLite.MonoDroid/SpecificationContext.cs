using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;

namespace NUnitLite
{
    public delegate void Establish(); 
    public delegate void Beacuse(); 
 


    public abstract class SpecificationContext
    {
        [TestFixtureSetUp] 
        public void Init()
        {

            Establish context = null;
            Beacuse of = null;


            foreach (var field in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.FieldType.Equals(typeof(Establish)))
                {
                    context = field.GetValue(this) as Establish;
                }
                else if (field.FieldType.Equals(typeof(Beacuse)))
                {
                    of = field.GetValue(this) as Beacuse;
                }
            }


            if (context != null)
            {
                context.Invoke();
            }
            if (of != null)
            {
              of.Invoke();
            };
        }

        
        //protected virtual Establish Context { get { return null; } }
        //protected virtual Beacuse Of { get { return null; } }
    }

}
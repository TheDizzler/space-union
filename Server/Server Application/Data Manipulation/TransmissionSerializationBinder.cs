using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manipulation
{
    internal sealed class TransmissionSerializationBinder : SerializationBinder
    {
        /// <summary>
        /// Bind to the assembly to the correct type.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="typeName">The type of the object.</param>
        /// <returns></returns>
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;
            try
            {
                string ToAssemblyName = assemblyName.Split(',')[0];
                Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in Assemblies)
                {
                    if (assembly.FullName.Split(',')[0] == ToAssemblyName)
                    {
                        typeToDeserialize = assembly.GetType(typeName);
                        break;
                    }
                }
            }
            catch (Exception e) { e.GetBaseException(); }
            return typeToDeserialize;
        }
    }
}

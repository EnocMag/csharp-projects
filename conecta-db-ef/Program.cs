using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Xml;
using ConectaDbEf;

Console.WriteLine("********************Bienvenido a la aplicación de ventas online********************");

int opcion = 0;
var context = new TiendaDbContext();

do {
    Console.WriteLine("===================================================================================");
    Console.WriteLine("Seleccione una opción:");
    Console.WriteLine("1. Consultar un cliente por id");
    Console.WriteLine("2. Crear un nuevo cliente");
    Console.WriteLine("3. Buscar un cliente por nombre");
    Console.WriteLine("4. Consultar un empleado por id");
    Console.WriteLine("5. Consultar el número de empleados");
    Console.WriteLine("6. Actualizar dirección de cliente");
    Console.WriteLine("7. Contratar un nuevo empleado");
    Console.WriteLine("8. Despedir empleado");
    Console.WriteLine("0. Salir");
    Console.WriteLine("===================================================================================");
    opcion = int.Parse(Console.ReadLine());
    if(opcion == 1)
    {
        Console.WriteLine("Favor de introducir el id del cliente a consultar");
        var idCliente = int.Parse(Console.ReadLine());

        var cte = context.Clientes.FirstOrDefault(x => x.Id == idCliente);
        if (cte != null)
        {
            Console.WriteLine($"Cliente encontrado: {cte.Nombre}, Email: {cte.Email}, Telefono: {cte.Telefono}, Direccion: {cte.Direccion}");
        }
        else
        {
            Console.WriteLine($"No se encontraron clientes con el id {idCliente}");
        }
    } 
    else if (opcion == 2)
    {
        
        Console.WriteLine("Favor de introducir el nombre del cliente");
        var nombre = Console.ReadLine();
        Console.WriteLine("Favor de introducir el email del cliente");
        var email = Console.ReadLine();
        Console.WriteLine("Favor de introducir el telefono del cliente");
        var telefono = Console.ReadLine();
        Console.WriteLine("Favor de introducir la direccion del cliente");
        var direccion = Console.ReadLine();
        var nuevoCliente = new Cliente
        {
            Nombre = nombre,
            Email = email,
            Telefono = telefono,
            Direccion = direccion
        };
        context.Clientes.Add(nuevoCliente);
        context.SaveChanges();
        Console.WriteLine($"Cliente {nuevoCliente.Nombre} creado con exito, id: {nuevoCliente.Id}");
    }
    else if (opcion == 3)
    {
        Console.WriteLine("Favor de introducir el nombre del cliente");
        var nombreCliente = Console.ReadLine();

        var cte = context.Clientes.FirstOrDefault(x => x.Nombre == nombreCliente);
        if (cte != null)
        {
            Console.WriteLine($"Cliente encontrado: {cte.Nombre}, Email: {cte.Email}, Telefono: {cte.Telefono}, Direccion: {cte.Direccion}");
        }
        else
        {
            Console.WriteLine($"No se encontraron clientes con el nombre {nombreCliente}");
        }
    }
    else if (opcion == 4)
    {
        Console.WriteLine("Favor de introducir el id del Empleado a consultar");
        var idEmpleado = int.Parse(Console.ReadLine());

        var emp = context.Empleados.FirstOrDefault(x => x.Id == idEmpleado);
        if (emp != null)
        {
            Console.WriteLine($"Empleado encontrado: {emp.Nombre}, Email: {emp.Email}, Puesto: {emp.Puesto}");
        }
        else
        {
            Console.WriteLine($"No se encontraron empleados con el id {idEmpleado}");
        }
    }
    else if (opcion == 5)
    {
        var empNum = context.Empleados.Count();
        Console.WriteLine($"Tenemos {empNum} empleados");
    }
    else if (opcion == 6)
    {
        Console.WriteLine("Favor de introducir el id del cliente a actualizar");
        var idCliente = int.Parse(Console.ReadLine());
        var cte = context.Clientes.FirstOrDefault(x => x.Id == idCliente);
        if (cte != null)
        {
            Console.WriteLine($"Cliente encontrado: {cte.Nombre}, Email: {cte.Email}, Telefono: {cte.Telefono}, Direccion: {cte.Direccion}");
            Console.WriteLine("Favor de introducir la nueva direccion del cliente");
            var nuevaDireccion = Console.ReadLine();
            cte.Direccion = nuevaDireccion;
            context.SaveChanges();
            Console.WriteLine($"Dirección actualizada a: {cte.Direccion}");
        }
        else
        {
            Console.WriteLine($"No se encontraron clientes con el id {idCliente}");
        }
    }
    else if (opcion == 7)
    {
        Console.WriteLine("Favor de introducir el nombre del empleado");
        var nombre = Console.ReadLine();
        Console.WriteLine("Favor de introducir el email del empleado");
        var email = Console.ReadLine();
        Console.WriteLine("Favor de introducir el puesto del empleado");
        var puesto = Console.ReadLine();
        var nuevoEmpleado = new Empleado
        {
            Nombre = nombre,
            Email = email,
            Puesto = puesto
        };
        context.Empleados.Add(nuevoEmpleado);
        context.SaveChanges();
        Console.WriteLine($"Empleado {nuevoEmpleado.Nombre} creado con exito, id: {nuevoEmpleado.Id}");
    }
    else if (opcion == 8)
    {
        Console.WriteLine("Introduce el id del empleado a despedir");
        var idEmpleado = int.Parse(Console.ReadLine());
        var emp = context.Empleados.FirstOrDefault(x => x.Id == idEmpleado);
        if (emp != null)
        {
            context.Empleados.Remove(emp);
            context.SaveChanges();
            Console.WriteLine($"Empleado {emp.Nombre} despedido con éxito... Por flojo!!");
        }
        else
        {
            Console.WriteLine($"No se encontró un empleado con el id {idEmpleado}");
        }
    }

} while (opcion > 0);
Console.WriteLine("Gracias por usar la aplicacion, hasta pronto!");


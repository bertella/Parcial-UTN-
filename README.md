# Gestor de Viajes de Agencia

Este proyecto es una aplicación de Windows Forms escrita en C# que permite gestionar información de viajes para una agencia de viajes. La aplicación se conecta a una base de datos local de SQL Server y utiliza dos tablas: "Transportes" y "Viajes".

## Funcionalidades

- **Interfaz Principal (`frmViaje`):** 
  - Carga datos desde la base de datos para mostrar en un ComboBox (`cboTransporte`) y una ListBox (`lstViajes`).
  - Permite al usuario seleccionar un transporte.
  - Muestra los viajes existentes y permite agregar nuevos viajes.

## Clase Viaje

La clase `Viaje` representa un objeto de viaje con los siguientes atributos:

- Código
- Destino
- Transporte
- Tipo
- Fecha

La aplicación facilita al usuario ingresar nuevos viajes, valida la entrada y guarda la información en la base de datos SQL Server.

## Requisitos

- .NET Framework
- SQL Server

## Instrucciones de Uso

1. Clona el repositorio.
2. Abre el proyecto en tu entorno de desarrollo (Visual Studio, por ejemplo).
3. Asegúrate de tener configurada la base de datos local y los requisitos previos.
4. Compila y ejecuta la aplicación.

¡Disfruta gestionando tus viajes con esta aplicación sencilla y práctica!

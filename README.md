# EstebanCamposDatos1Tarea1
Tarea 1 Datos 1 30/07/24 Esteban Campos 2022207705.

------------------------------------------------------------------------------
INSTRUCCIONES DE USO
1- Se necesita .NET para compilar el codigo. Se puede encontrar en https://dotnet.microsoft.com/en-us/download/dotnet
2- Abrir la carpeta POO_mensajeria.
3- Abrir la terminal en la carpeta.
4- Ingresar el comando "dotnet run -port <puerto>" donde <puerto> es un entero entre 1000 y 9999 que indica el puerto de escucha.
5- El programa indicara "Escriba su mensaje".
	- El formato del mensaje es un string que termina en cuatro digitos que indica el puerto al que se envia el mensaje.
		Ej.:
			Hola!1990
			Hola! 1990
			My numero es Beechwood 45-7891990
			My numero es Beechwood 45-789 1990
			787-E8811993
			787-E881 1993
		Otros strings devuelven los errores: (Mensaje invalido) o (Puerto invalido)
6- Para abrir otra ventana, se abre otra terminal en la misma direccion y se usa "dotnet run -port <puerto2>" con otro puerto.

		

# ParkingAppWebApi

This is my repository for a school project. 
The project contains: 
 - Database
 - Web Api 1(this repository)
 - Mobile app(![ParkoviskoCheckingAPP](https://github.com/f1l1ph/PythonBackend-parkingApp) repository)
 - Web Api 2(![PythonBackend-parkingApp](https://github.com/f1l1ph/ParkoviskoCheckingAPP) repository)

Project was made to check on whether cars on school parking lots belong to students and teachers or to someone else who isn't related to school.
License plate numbers and details are all stored in database and Web Api is communicating with database.

---
Web Api details:
 - Project type: Asp.Net core WebApi
 - .Net version: 8
 - IDE: Visual studio community 2022 preview
 - Libraries/Tools:
   - Entity Framework Core - for working with database
   - Refit - for communicating with second WebApi
   - Authentification - JWT(JSON Web Token), Sha256 encoding
   - Validation - Regex and Fluent validation

   
<em>Web Api 1 is kind of a middle man</em><br>
![parking-simplified](https://github.com/f1l1ph/ParkingAppWebApi/assets/50553234/ffa188e6-df00-456d-b61e-b5735511179c)
<br><br>
<em>Screenshot of all Api calls from swagger</em><br>
![swagger_Screen](https://github.com/f1l1ph/ParkingAppWebApi/assets/50553234/1de1b9d9-5586-4c3c-8393-e560b3ee30fa)

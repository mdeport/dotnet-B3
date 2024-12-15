# Tp C#

## Package : 
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore - 8.0.8
    - Microsoft.AspNetCore.Identity.UI - 8.0.8
    - Microsoft.EntityFrameworkCore.Design - 8.0.1
    - Pomelo.EntityFrameworkCore.MySql - 8.0.1

## Configuration de la database : 
Dans le fichier `appsettings.json` il faut modifier la connection string pour se connecter à la base de données.
```bash
    "DefaultConnection": "server=localhost;database=RPIDEV;user=root;password=example"
```

## Réaliser les migrations :

```bash
    dotnet ef migrations add NameMigration
    dotnet ef database update
```

## Explication de l'application

Dans l'application il y a 2 types d'utilisateurs :
- Les teachers
- Les students

Les teachers peuvent se créer un compte et se connecter.
Les students peuvent uniquement se connecter.

Les teachers peuvent gérer les students et les évènements.(créer, modifier supprimer.)

Les students peuvent s'inscrire à un évènement et se désinscrire.

## Event

Il peuvent rechercher des évènements en fonction du titre a l'aide de la barre de recherche.
Ou il peuvent trier les évènements par date de début ou de fin et par ordre alphabetique en fonction du titre de l'évènement.

# FinancialAssetManager
# Sobre o projeto

FinancialAssetManager é um projeto para consultar dados de uma API Externa do Yahoo com dados financeiros e realizar cálculos de variação
D-1 e relacionado à primeira data do pregão ( data de abertura de mercado para vendas, compras ).

## Tecnologias utilizadas
Back-end:
- C#;
- .NET 6;
- ASP.NET Core;
- API;
- xUnit - Testes Unitários;
- MongoDB;

## Arquitetura
- DDD - Domain-Driven Design;


## PRÉ-REQUISITOS
- .NET SDK 6.0.0 ou superior.
- Visual Studio 2022 ou Visual Studio Code.
- MongoDB.

## COMO BAIXAR E EXECUTAR O PROJETO

### Sugestão para utilizar o mongoDB: Utilize a IDE "MongoDB Compass"

### Clique "Create Database"
```
https://prnt.sc/mwRL50Zg8HNI
```
### Preencha os campos como mostra a imagem abaixo
```
https://prnt.sc/nYAzymiVQI-W
```
### Crie o index para o field { "Resulta.Meta.Symbol" } indo em "Index" > "Create Index"
```
https://prnt.sc/OOzM_FIhzgO3
```
### OBS: Indexar um campo torna a consulta mais otimizada, quando se tem uma collection com muitos documentos.

### Crie uma pasta em documentos chamada "FinancialProject" e acesse-a.

```
cd Documents
mkdir FinancialProject
cd FinancialProject
```

### clone o repositório - use o comando git clone 
```
git clone https://github.com/ElitonSantana/FinancialAssetManager.git
```
### acesse a pasta "FinancialAssetManager"

```
cd FinancialAssetManager
cd FinancialAssetManager
```

### Use o comando dotnet run no terminal para compilar e executar o projeto C#.
```
dotnet run
```

### No terminal irá aparecer o a porta, a URL é a seguinte: ( caso necessário, substitua a porta para a informada no terminal)
```
http://localhost:5265/swagger/index.html
```


# Autor

Eliton Alves de Santana
https://www.linkedin.com/in/eliton-alves-de-santana-69a492198/
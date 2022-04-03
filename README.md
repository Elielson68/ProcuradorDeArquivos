# ProcuradorDeArquivos
Um programa feito em C# que procura algum arquivo dado um path e o nome do arquivo. Utiliza o Parallel para cada pasta que ele precisa buscar.

Vídeo explicativo:

https://youtu.be/TJhvMcECCcE

tendo seu projeto dotnet iniciado basta rodar o comando

```
dotnet run path="caminho/do/arquivo" file="nomeDoArquivo"
```

path e file são obrigatórios, outros comandos opcionais são:

extension = Verifica a extensão do arquivo

date = Verifica a data de criação do arquivo

bytes = Verifica o tamanho em bytes do arquivo

content = Verifica se o conteúdo existe dentro do arquivo


Uma busca mais avançada poderia ser:

```
dotnet run path="C:/Users/MacacoLoco/" file="MinhaSanidade" extension=".txt" date="03/04/2022" bytes="1024" content="Eu to ficando locom seu doutor!"
```

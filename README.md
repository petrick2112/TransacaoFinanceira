# 💰 Projeto Transação Financeira

## Visão Geral
O **Sistema de Transação Financeira** simula transferências financeiras entre contas usando um repositório em memória. 💵

## Estrutura do Projeto
```
TransacaoFinanceira/
├── Models/
│   └── Account.cs                   # Entidade de conta bancária
├── Repositories/
│   ├── IAccountRepository.cs        # Interface do repositório
│   └── InMemoryAccountRepository.cs # Implementação em memória
├── Services/
│   └── TransactionService.cs        # Regras de negócio (transferências)
├── Utils/
│   └── SimpleLogger.cs              # Logger simples para console
├── TransacaoFinanceira.Tests/
│   └── TestTransacaoFinanceira.cs   # Testes básicos
└── TransacaoFinanceira.cs           # Ponto de entrada do sistema
└── TransacaoFinanceira.csproj       # Arquivo de projeto em C#
```

## Funcionalidades ✨
- **Contas pré-cadastradas em memória**: O sistema inicia com várias contas fictícias, cada uma com saldo inicial e ps dados ficam armazenados em memória.
- **Processamento de transações**: O programa recebe uma lista de transações com:
```
correlation_id → identificador único da transação
datetime → data/hora (só para registro)
conta_origem → conta de onde sai o valor
conta_destino → conta que recebe o valor
valor → valor a transferir
```
- **As transações são ordenadas por correlation_id e executadas em sequência**

- **Atualização de saldo**: Se válida, a operação débita a conta de origem e credita a conta de destino e é atualizado imediatamente
- **Logs**: Para cada transação, é exibido no console.

## Como Usar ▶️
Clone o repositório e instale o "C# Dev Kit" no VS Code.
Abra o terminal no VS Code e navegue até a pasta do projeto.
Para compilar e executar o projeto, digite o comando 'dotnet run'

## Contribua! 🤝
Sugestões e melhorias são bem-vindas! Envie um pull request ou abra uma issue.

## Licença 📄
Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais
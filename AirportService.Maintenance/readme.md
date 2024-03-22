# запуск на 1 машине
<details>
    <summary>Конфигурация для запуска InMemory</summary>

```json
  "FeatureManagement": {
    "IsOxygen": true,
    "IsDispatcher": true,
    "IsFuel": true,
    "IsMover": true,
    "IsBaggageBeltLoader": true,
    "IsStartingEngines": true,
    "IsLavatory": true,
    "IsFood": true,
    "IsWater": true,
    "IsCleaning": true,
    "IsElectricity": true,
    "IsBaggageTransport": true,
    "IsPassengerBridge": true,
    "IsPassengerCoordination": true
  },

  "BrokerConfig": {
    "UseLocal": true,
    "UseRabbitMq": false,
    "RabbitMqHost": "localhost",
    "RabbitMqLogin": "guest",
    "RabbitMqPassword": "guest",
    "UseRabbitMqUseSSL": true
  },
```

</details>

# запуск в распределённой системе

<details>
    <summary>Брокер сообщений</summary>
RabbitMq в докер

```
docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

</details>
<details>
    <summary>Конфигурируем MassTransit</summary>

```json
  "BrokerConfig": {
    "UseLocal": false,
    "UseRabbitMq": true,
    "RabbitMqHost": "localhost",
    "RabbitMqLogin": "guest",
    "RabbitMqPassword": "guest",
    "UseRabbitMqUseSSL": true
  },
```

Если надо, то меняем адрес зостовой машины кролика
</details>

## Конфигурирование для 2 машин
Помимо указания host машины кролика. надо запустить по-разному службы.
<details>
    <summary>Диспетчер</summary>

```json
  "FeatureManagement": {
	"IsDispatcher": true,
    "IsOxygen": false,
    "IsFuel": false,
    "IsMover": false,
    "IsBaggageBeltLoader": false,
    "IsStartingEngines": false,
    "IsLavatory": false,
    "IsFood": false,
    "IsWater": false,
    "IsCleaning": false,
    "IsElectricity": false,
    "IsBaggageTransport": false,
    "IsPassengerBridge": false,
    "IsPassengerCoordination": false
  }
```

</details>

<details>
    <summary>Службы</summary>

```json
  "FeatureManagement": {
    "IsDispatcher": false,
    "IsOxygen": true,
    "IsFuel": true,
    "IsMover": true,
    "IsBaggageBeltLoader": true,
    "IsStartingEngines": true,
    "IsLavatory": true,
    "IsFood": true,
    "IsWater": true,
    "IsCleaning": true,
    "IsElectricity": true,
    "IsBaggageTransport": true,
    "IsPassengerBridge": true,
    "IsPassengerCoordination": true
  }
```

</details>

## Тест

Для тестирование отправить на хосте диспетчера

```curl
curl --location --request POST 'https://localhost:7194/api/Dispatcher/plane/stage/01/start?guid=dbced671-52bf-4a89-96dd-444fb0952b22' \
--header 'accept: text/plain'
```

Или используя swagger вызвать метод 
```
/api/Dispatcher/plane/stage/01/start
```
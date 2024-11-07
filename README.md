
# FileStorage

This a task for youxel lead position, it is contain simple microservice to handle files for other services it support functions like upload, retrive and delete.

    1) Functional and Non-Functional Requirements
        Functional Requirements
            - File Upload: Enable uploading of files of any type and size (within reasonable limits).
            - File Retrieval: Allow retrieval of files by ID, path, or specific metadata.
            - File Metadata: Store and retrieve metadata (such as file size, upload date, file type) for efficient querying and auditing.

        Non-Functional Requirements
            - Scalability: The service should scale horizontally to support large file volumes and high request loads.
            - Performance: Ensure low-latency for common operations like file uploads and retrievals.
            - Reliability: High availability and fault tolerance should be built-in, so files are accessible at all times.
            - Durability: Files should be stored with high durability to minimize risk of data loss.

    2) High-Level and Low-Level Design for the Microservice
        High-Level Design
            - API Gateway: Provides a centralized entry point for file operations (upload, download, delete) and handles routing, throttling, and security.
            - Storage Service: The core service responsible for managing file storage and retrieval. It interacts with a storage backend (e.g., object storage) and a database for metadata storage.
            - Database for Metadata: A relational or NoSQL database to store file metadata, supporting fast lookups and indexing.
        
        Low-Level Design
            - FileStorage Api To Handle Add, download or Delete Files and controle security




## Technolgies

 - [C#]
 - [Asp.net core web api]
 - [MediatR]
 - [Grpc]
 - [RabbitMQ]
 - [MongoDb]
 - [CQRS]
 - [Clean Artch]



## To Use Grpc Functions From Other Method Follow this.

1- add package Grpc.Net.Client
2- add package Grpc.Tools

3- create this service

public class FileService
{
    private readonly StorageService.StorageServiceClient _storageClient;

    public FileService(string grpcAddress)
    {
        var channel = GrpcChannel.ForAddress(grpcAddress);
        _storageClient = new StorageService.StorageServiceClient(channel);
    }

    public async Task<string> UploadFileAsync(string fileName, byte[] content, string contentType)
    {
        var request = new UploadFileRequest
        {
            FileName = fileName,
            ContentType = contentType,
            Content = Google.Protobuf.ByteString.CopyFrom(content)
        };

        var response = await _storageClient.UploadFileAsync(request);
        return response.FileId;
    }

    public async Task<byte[]> GetFileAsync(string fileId)
    {
        var request = new GetFileRequest { FileId = fileId };
        var response = await _storageClient.GetFileAsync(request);
        return response.Content.ToByteArray();
    }

    public async Task<bool> DeleteFileAsync(string fileId)
    {
        var request = new DeleteFileRequest { FileId = fileId };
        var response = await _storageClient.DeleteFileAsync(request);
        return response.Success;
    }
}




## To Start Consuming RabbitMq File Event Messages Follow this.

1- add RabbitMQ.Client;
2- add RabbitMQ.Client.Events;
3 - create this class

public class FileEventConsumer
{
    private readonly RabbitMqConnectionFactory _connectionFactory;

    public FileEventConsumer(RabbitMqConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void StartConsuming()
    {
        var connection = _connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("file-events", ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queueName, "file-events", string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Deserialize and handle the events
            var eventType = e.RoutingKey; // You can differentiate events by the routing key if needed
            if (eventType == "FileAddedEvent")
            {
                var fileAddedEvent = JsonConvert.DeserializeObject<FileAddedEvent>(message);
                // Handle the FileAddedEvent logic here
            }
            else if (eventType == "FileDeletedEvent")
            {
                var fileDeletedEvent = JsonConvert.DeserializeObject<FileDeletedEvent>(message);
                // Handle the FileDeletedEvent logic here
            }
        };

        channel.BasicConsume(queueName, true, consumer);
    }
}



## Authors

- [Ahmed Raheem]


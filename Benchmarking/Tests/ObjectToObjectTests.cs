using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using Mapster;
using Nelibur.ObjectMapper;

namespace Benchmarking.Tests;

[MemoryDiagnoser]
public class ObjectToObjectTests
{
    [Benchmark]
    public void MapsterTest()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = payment.Adapt<PaymentEntity>();
    }

    private readonly AutoMapper.Mapper _autoMapper;
    public ObjectToObjectTests()
    {
        //Setup AutoMapper
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Payment, PaymentEntity>());
        _autoMapper = new AutoMapper.Mapper(config);
        //Setup TinyMapper
        TinyMapper.Bind<Payment, PaymentEntity>();
    }

    [Benchmark]
    public void Inline()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = new PaymentEntity()
        {
            Amount = payment.Amount,
            Created = payment.Created,
            Id = payment.Id,
            Status = payment.Status
        };
    }

    [Benchmark]
    public void Reflection()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = ReflectionMapper.MapObjects<Payment, PaymentEntity>(payment);
    }

    [Benchmark]
    public void AutoMapper()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = _autoMapper.Map<PaymentEntity>(payment);
    }

    [Benchmark]
    public void TinyMapperTest()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = TinyMapper.Map<PaymentEntity>(payment);
    }



    [Benchmark]
    public void Mapster()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = payment.Adapt<PaymentEntity>();
    }


    [Benchmark]
    public void ValueInjecter()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = Omu.ValueInjecter.Mapper.Map<PaymentEntity>(payment);
    }


    [Benchmark]
    public void FastMapper()
    {
        var payment = new Payment()
        {
            Amount = 120,
            Created = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Status = "Created"
        };
        var paymentEntity = TypeAdapter.Adapt<Payment, PaymentEntity>(payment);
    }
}

public class PaymentEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}

public class Payment
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}

public static class ReflectionMapper
{
    public static TTo MapObjects<TFrom, TTo>(TFrom sourceObject) where TTo : new()
    {
        TTo targetObject = new TTo();
        Type sourceType = sourceObject.GetType();
        PropertyInfo[] sourceProperties = sourceType.GetProperties();
        PropertyInfo[] targetProperties = typeof(TTo).GetProperties();

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            PropertyInfo targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
            if (targetProperty != null && targetProperty.CanWrite)
            {
                object value = sourceProperty.GetValue(sourceObject);
                targetProperty.SetValue(targetObject, value);
            }
        }

        return targetObject;
    }
}
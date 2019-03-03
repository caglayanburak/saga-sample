using System;
using System.Threading.Tasks;
using Automatonymous;
using Commonlib;
using MassTransit.Saga;

namespace LCSaga
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        // public State CurrentState { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string CurrentState { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class OrderReceivedEvent : IOrderReceivedEvent
    {
        private readonly OrderSagaState _orderSagaState;

        public OrderReceivedEvent(OrderSagaState orderSagaState)
        {
            _orderSagaState = orderSagaState;
        }

        public Guid CorrelationId
        {
            get
            {
                return _orderSagaState.CorrelationId;
            }
        }

        public string OrderCode
        {
            get
            {
                return _orderSagaState.OrderCode;
            }
        }

        public int OrderId
        {
            get
            {
                return _orderSagaState.OrderId;
            }
        }
    }

    public class OrderSaga : MassTransitStateMachine<OrderSagaState>
    {
        public State Received { get; set; }
        public State Processed { get; set; }

        public Event<IOrderReceivedEvent> OrderCommand { get; set; }
        public Event<IOrderProcessedEvent> OrderProcessed { get; set; }

        public OrderSaga()
        {
            InstanceState(s => s.CurrentState);

           Event(() => OrderCommand, x => x.CorrelateBy(state => state.OrderCode, context => context.Message.OrderCode)
                .SelectId(context => Guid.NewGuid()));

            Event(() => OrderProcessed, cec => cec.CorrelateById(selector =>
                        selector.Message.CorrelationId));

            Initially(
                When(OrderCommand)
                    .Then(context =>
                    {
                        context.Instance.Created = DateTime.Now;
                        context.Instance.Updated = DateTime.Now;
                        context.Instance.UserName = "burak";
                        context.Instance.OrderCode = context.Data.OrderCode;
                        context.Instance.OrderId = context.Data.OrderId;
                        context.Instance.CorrelationId = context.Data.CorrelationId;
                    })
                    .ThenAsync(
                        context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is received..")
                    )
                    .Publish(context => new OrderReceivedEvent(context.Instance))
                    .TransitionTo(Received)
                );


            During(Received,
                When(OrderProcessed)
                .Then(context =>
                {
                    context.Instance.OrderCode = context.Data.OrderCode;
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.CorrelationId = context.Data.CorrelationId;
                })
                .ThenAsync(
                    context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is processed.."))
                .Finalize()
                );

            SetCompletedWhenFinalized();
        }
    }
}
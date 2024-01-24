using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirBnB.Domain.Common.Events;

namespace AirBnB.Application.Common.EventBus;

public interface IEvenBusBroker
{
    ValueTask PublishLocalAsync<TEvent>(TEvent command) where TEvent : IEvent;

    ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

    ValueTask SubscribeAsync<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;

}

import React, { useState } from 'react';

function EventExample() {
  const [count, setCount] = useState(5);

  // 1. Increment logic with multiple methods
  const handleIncrement = () => {
    setCount(prev => prev + 1);
    sayHello();
  };

  const sayHello = () => {
    alert("Hello! Counter incremented.");
  };

  const handleDecrement = () => {
    setCount(prev => prev - 1);
  };

  // 2. Say welcome function
  const sayWelcome = (message) => {
    alert(message);
  };

  // 3. Synthetic event example
  const handleClick = () => {
    alert("I was clicked");
  };

  return (
    <div>
      <h2>{count}</h2>
      <button onClick={handleIncrement}>Increment</button><br /><br />
      <button onClick={handleDecrement}>Decrement</button><br /><br />
      <button onClick={() => sayWelcome("welcome")}>Say welcome</button><br /><br />
      <button onClick={handleClick}>Click on me</button>
    </div>
  );
}

export default EventExample;
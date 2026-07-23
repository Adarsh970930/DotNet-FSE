import React from 'react';

class Cart extends React.Component {
  render() {
    return (
      <div>
        <h3>{this.props.itemname}</h3>
        <p>Price: ₹{this.props.price}</p>
      </div>
    );
  }
}

Cart.defaultProps = {
  itemname: "Unknown Item",
  price: 0
};

export default Cart;
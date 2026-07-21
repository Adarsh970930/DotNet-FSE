// Getuser.js
import React, { Component } from 'react';

class Getuser extends Component {
  constructor(props) {
    super(props);
    this.state = {
      user: null,
      isLoading: true,
      error: null
    };
  }

  async componentDidMount() {
    try {
      const response = await fetch('https://api.randomuser.me/');
      const data = await response.json();
      this.setState({ user: data.results[0], isLoading: false });
    } catch (err) {
      this.setState({ error: 'Failed to fetch user', isLoading: false });
    }
  }

  render() {
    const { user, isLoading, error } = this.state;

    if (isLoading) {
      return <p>Loading user...</p>;
    }

    if (error) {
      return <p style={{ color: 'red' }}>{error}</p>;
    }

    return (
      <div style={{ textAlign: 'center', marginTop: '30px' }}>
        <h2>User Information</h2>
        <p><strong>Title:</strong> {user.name.title}</p>
        <p><strong>First Name:</strong> {user.name.first}</p>
        <img src={user.picture.large} alt="User" style={{ borderRadius: '50%' }} />
      </div>
    );
  }
}

export default Getuser;
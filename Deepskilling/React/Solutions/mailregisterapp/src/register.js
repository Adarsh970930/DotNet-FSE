// register.js
import React, { useState } from 'react';

function Register() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: ''
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });

    // Live validation
    const { name, value } = e.target;
    let newErrors = { ...errors };

    if (name === 'name' && value.length < 5) {
      newErrors.name = 'Name must be at least 5 characters long';
    } else if (name === 'email' && (!value.includes('@') || !value.includes('.'))) {
      newErrors.email = 'Email must include "@" and "."';
    } else if (name === 'password' && value.length < 8) {
      newErrors.password = 'Password must be at least 8 characters long';
    } else {
      delete newErrors[name]; // remove previous error
    }

    setErrors(newErrors);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const validationErrors = {};

    if (formData.name.length < 5) {
      validationErrors.name = 'Name must be at least 5 characters long';
    }

    if (!formData.email.includes('@') || !formData.email.includes('.')) {
      validationErrors.email = 'Email must include "@" and "."';
    }

    if (formData.password.length < 8) {
      validationErrors.password = 'Password must be at least 8 characters long';
    }

    setErrors(validationErrors);

    if (Object.keys(validationErrors).length === 0) {
      alert('Registration successful!');
      setFormData({ name: '', email: '', password: '' });
    }
  };

  return (
    <div style={{ textAlign: 'center', marginTop: '40px' }}>
      <h2 style={{ color: 'blue' }}>User Registration</h2>
      <form onSubmit={handleSubmit}>
        <div style={{ margin: '10px' }}>
          <label>Name:&nbsp;</label>
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleChange}
          />
          <br />
          <span style={{ color: 'red' }}>{errors.name}</span>
        </div>

        <div style={{ margin: '10px' }}>
          <label>Email:&nbsp;</label>
          <input
            type="text"
            name="email"
            value={formData.email}
            onChange={handleChange}
          />
          <br />
          <span style={{ color: 'red' }}>{errors.email}</span>
        </div>

        <div style={{ margin: '10px' }}>
          <label>Password:&nbsp;</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
          <br />
          <span style={{ color: 'red' }}>{errors.password}</span>
        </div>

        <button type="submit">Register</button>
      </form>
    </div>
  );
}

export default Register;
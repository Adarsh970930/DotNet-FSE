import { Component } from '@angular/core';

@Component({
  selector: 'app-not-found',
  standalone: true,
  template: `
    <div class="text-center p-5">
      <h1 class="display-1">404</h1>
      <p class="lead">Page Not Found</p>
      <a href="/" class="btn btn-primary">Go to Home</a>
    </div>
  `
})
export class NotFoundComponent {}
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { CustomerOverviewComponent } from './pages/customer-overview/customer-overview.component';

const routes: Routes = [
  { path: '', component: CustomerOverviewComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CustomerRouterModule { }

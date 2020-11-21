import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MusicComponent } from './music.component';
import { MusicDetailComponent } from './music-detail/music-detail.component';

const routes: Routes = [
  {
    path: "music",
    component: MusicComponent
  },
  {
    path: "music/:id",
    component: MusicDetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MusicRoutingModule { }

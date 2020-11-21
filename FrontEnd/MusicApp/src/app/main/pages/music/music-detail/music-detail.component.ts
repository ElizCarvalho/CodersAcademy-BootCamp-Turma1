import { Component, OnInit } from '@angular/core';
import { MusicService } from 'app/services/music.service';
import { ActivatedRoute, Router } from '@angular/router';
import Album from 'app/model/album';

@Component({
  selector: 'app-music-detail',
  templateUrl: './music-detail.component.html',
  styleUrls: ['./music-detail.component.scss']
})
export class MusicDetailComponent implements OnInit {

  albumId: String = undefined;
  album: Album = undefined;

  constructor(
    private musicService: MusicService, 
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.albumId = this.activatedRoute.snapshot.paramMap.get("id");
    this.musicService.getAlbumDetails(this.albumId).subscribe(data => {
      this.album = data;
    });
  }

  back(){
    this.router.navigate(["music"]);
  }

}

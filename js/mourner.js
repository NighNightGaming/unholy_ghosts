Mourner = function(game) {
  this.game = game;
  this.sprite = null;
}

Mourner.prototype = {
  preload: function() {
    this.game.load.image('mournerF', './assets/female_mourner_side.png');
    this.game.load.image('mournerM', './assets/male_mourner_side.png');
  },

  create: function(gender) {
    this.health = 5;
    this.player = player;
    this.alive = true;
    if (gender === "male") {
      this.sprite = game.add.sprite(game.rnd.integerInRange(0, 300),475,'mournerM');
    }
    else if (gender === "female") {
      this.sprite = game.add.sprite(game.rnd.integerInRange(0, 300),475,'mournerF');
    }
    this.sprite.anchor.setTo(0.5, 0.5);
    this.sprite.body.immovable = false;
    this.sprite.body.collideWorldBounds = true;
    this.sprite.body.bounce.setTo(1,1);
    this.facing = "right";
  },
  /*
  TODO
  update: function() {
    //enemy to follow player
    this.sprite.body.velocity = 0;

    if (this.body.x > player.body) {
      //move to the left
      this.sprite.body.velocity.x = -150;
      if (this.facing != 'left') {
        this.sprite.scale.x *= -1;
        this.facing = 'left';
      }
    }
    else if (his.body.x < player.body) {
      //move to the right
      this.sprite.body.velocity.x = 150;
      if (this.facing != 'right') {
        this.sprite.scale.x *= -1;
        this.facing = 'right';
      }
    }
  },
  */
  damage: function () {
    this.health -= 1;
    if (this.health <= 0) {
      this.alive = false;
      this.sprite.kill();
      return true;
    }
    return false;
  }

}
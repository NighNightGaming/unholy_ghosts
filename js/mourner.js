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
    this.alive = true;

    if (gender === "male") {
      this.sprite = game.add.sprite(game.rnd.integerInRange(0, 275),400,'mournerM');
      this.facing = "right";
    }
    else if (gender === "female") {
      this.sprite = game.add.sprite(game.rnd.integerInRange(520, 800),407,'mournerF');
      this.sprite.scale.x *= -1;
      this.facing = "left";
    }
    this.sprite.anchor.setTo(0.5, 0.5);
    this.game.physics.enable(this.sprite);
    this.sprite.body.immovable = false;
    this.sprite.body.collideWorldBounds = true;
    this.sprite.body.bounce.setTo(1,1);
  },
  //TODO
  update: function(user) {
    //enemy to follow target
    this.sprite.body.velocity.x = 0;

    if (this.sprite.body.x -10 > user.sprite.body.x) {
      //move to the left
      this.sprite.body.velocity.x = -150;
      if (this.facing != 'left') {
        this.sprite.scale.x *= -1;
        this.facing = 'left';
      }
    }
    else if (this.sprite.body.x + 10 < user.sprite.body.x) {
      //move to the right
      this.sprite.body.velocity.x = 150;
      if (this.facing != 'right') {
        this.sprite.scale.x *= -1;
        this.facing = 'right';
      }
    }
  },

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
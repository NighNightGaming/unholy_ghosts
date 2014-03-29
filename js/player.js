//player class
Player = function (game) {
  this.game = game;
  this.sprite = null;
  this.cursors = null;
}

Player.prototype = {
  preload: function () {
    this.game.load.image('ghost', './assets/ghost_sprite.png');
    this.game.load.image('possessedHostM', './assets/male_zombie_side.png');
    this.game.load.image('possessedHostF', './assets/female_mourne_zombie.png');
  },

  create: function() {
    this.health = 5;
    this.alive = true;
    this.sprite = game.add.sprite(400, 420,'ghost');
    this.game.physics.enable(this.sprite);
    this.sprite.anchor.setTo(0.5,0.5);
    this.sprite.body.collideWorldBounds = true;
    this.sprite.body.bounce.y = 0.5;
    this.facing = "right";
    this.cursors = game.input.keyboard.createCursorKeys();
    console.log("Notice: input being listened");
  },

  update: function() {
    //player movement reset
    this.sprite.body.velocity.x = 0;
    if (this.cursors.left.isDown) {
      //move to the left
      this.sprite.body.velocity.x = -150;
      if (this.facing != 'left') {
        this.sprite.scale.x *= -1;
        this.facing = 'left';
      }
    }
    else if (this.cursors.right.isDown) {
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
};
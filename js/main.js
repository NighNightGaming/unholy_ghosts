//initializing game
var game = new Phaser.Game(800, 600, Phaser.CANVAS, '', { preload: preload,
create: create, update: update, render: render});

//my vars
var floor = null;
var mounds = null;
var mourner = null;
var mournerGroup = null;
var player = null;

//loading up the assets
function preload() {

  game.load.image('sky', 'assets/sky.png');

  player = new Player(game);
  player.preload();

  mourner = new Mourner(game);
  mourner.preload();

  game.load.image('mound', 'assets/mound.png');
  game.load.image('platform', 'assets/platform.png');
  game.load.image('coffin', 'assets/coffin.png');

}


function create() {

  //word creation
  game.add.sprite(0,0, 'sky');
  game.add.sprite(400, 425, 'platform').anchor.setTo(0.5, 0.5);
  game.add.sprite(410, 450, 'coffin').anchor.setTo(0.5, 0.5);
  floor = new Phaser.Rectangle(0, 525, 800, 75);

  mounds = game.add.group();
  for (var x = 0; x < 6; x+= 1) {
    mounds.create(game.rnd.integerInRange(25, 775), 485, 'mound');
  }
  
  //creating an array of mourners to start
  mournerGroup = [];
  for (var i = 0; i < 1; i+=1) {
    mournerGroup.push(mourner.create('male'));
  }

  //player creation
  player.create();
}

function update() {
  player.update();
}

function render() {
  game.debug.renderRectangle(floor, '#23A315');

}
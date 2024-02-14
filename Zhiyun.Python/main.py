from args import set_args
from json import loads
from train import train


def main(args):
    with open(args.config, 'r') as f:
        config = loads(f.read())
    task = config['Task']
    if task == 'train':
        train(config)


if __name__ == "__main__":
    arg = set_args()
    main(arg)

import { Link } from 'react-router-dom';
import { Button, Header, Icon, Segment } from 'semantic-ui-react';

export default function NotFound() {
  return (
    <Segment placeholder>
      <Header icon>
        <Icon name="search" />
        Увы, такой страницы больше нет...
      </Header>
      <Segment.Inline>
        <Button as={Link} to="/" primary>
          Назад на главную
        </Button>
      </Segment.Inline>
    </Segment>
  )
}

import React from "react";
import ReactDOM from "react-dom";
import registerServiceWorker from "./registerServiceWorker";

import { Client as Styletron } from "styletron-engine-atomic";
import { Provider as StyletronProvider } from "styletron-react";
import ApolloClient from "apollo-boost";
import { BaseProvider } from "baseui";
import { ApolloProvider } from "@apollo/react-hooks";
import { theme } from "./theme";
import App from "./App";

const client = new ApolloClient({
  uri: process.env.REACT_APP_API_URL
});

function Index() {
  const engine = new Styletron();

  return (
    <ApolloProvider client={client as any}>
      <StyletronProvider value={engine}>
        <BaseProvider theme={theme}>
          <App />
        </BaseProvider>
      </StyletronProvider>
    </ApolloProvider>
  );
}

ReactDOM.render(<Index />, document.getElementById("root"));
registerServiceWorker();

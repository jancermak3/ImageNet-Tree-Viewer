import React, { useState, useEffect } from 'react';
import { fetchTreeData } from './services/treeService';
import { TreeNode } from './types/TreeNode';
import TreeView from './components/TreeView';
import ErrorMessage from './components/ErrorMessage';
import { ThemeProvider, createTheme, CssBaseline, Container, Typography, CircularProgress, Box, TextField } from '@mui/material';

const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

const App: React.FC = () => {
  const [treeData, setTreeData] = useState<TreeNode | null>(null);
  const [filteredData, setFilteredData] = useState<TreeNode | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [searchQuery, setSearchQuery] = useState<string>('');
  useEffect(() => {
    const loadData = async () => {
      try {
        const data = await fetchTreeData();
        setTreeData(data);
        setFilteredData(data);
      } catch (err) {
        setError('Failed to fetch tree data. Please try again later.');
      } finally {
        setIsLoading(false);
      }
    };

    loadData();
  }, []);

  useEffect(() => {
    if (searchQuery === '') {
      setFilteredData(treeData);
    } else {
      const filterTree = (node: TreeNode): TreeNode | null => {
        if (node.name.toLowerCase().includes(searchQuery.toLowerCase())) {
          return node;
        }
        if (node.children) {
          const filteredChildren = node.children.map(filterTree).filter(child => child !== null) as TreeNode[];
          if (filteredChildren.length > 0) {
            return { ...node, children: filteredChildren };
          }
        }
        return null;
      };

      const filtered = treeData ? filterTree(treeData) : null;
      setFilteredData(filtered);
    }
  }, [searchQuery, treeData]);

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Container maxWidth="lg">
        <Box sx={{ my: 4 }}>
          <Typography 
            variant="h2" 
            component="h1" 
            gutterBottom 
            align="center" 
            sx={{ 
              fontWeight: 'bold', 
              fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif'
            }}
          >
            ImageNet Tree View
          </Typography>
          <Typography variant="body1" align="center" sx={{ mb: 3 }}>
            This page displays the taxonomy system <a href="https://github.com/tzutalin/ImageNet_Utils/blob/master/detection_eval_tools/structure_released.xml" target="_blank" rel="noopener noreferrer">data</a> from ImageNet.
            <br />
            Use the search bar to filter the items. Only the items that contain the search query and their parent nodes are shown.
          </Typography>
          <TextField
            label="Search"
            variant="outlined"
            fullWidth
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            sx={{ mb: 4 }}
          />
          {isLoading ? (
            <Box display="flex" justifyContent="center" alignItems="center" height="50vh">
              <CircularProgress size={60} />
            </Box>
          ) : error ? (
            <ErrorMessage message={error} />
          ) : !filteredData ? (
            <Typography variant="body1" align="center">No data available.</Typography>
          ) : (
            <Box height="calc(100vh - 200px)">
              <TreeView node={filteredData} />
            </Box>
          )}
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default App;
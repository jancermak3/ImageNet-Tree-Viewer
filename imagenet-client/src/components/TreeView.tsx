import React, { useState } from 'react';
import { 
  Accordion, AccordionSummary, AccordionDetails, 
  Typography, Box, Chip, IconButton 
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import FolderIcon from '@mui/icons-material/Folder';
import ImageIcon from '@mui/icons-material/Image';
import { TreeViewProps } from '../types/TreeViewProps';
import { TreeNode } from '../types/TreeNode';

const TreeView: React.FC<TreeViewProps> = ({ node }) => {
  const [expanded, setExpanded] = useState<string[]>([]);

  const handleExpand = (nodeName: string) => {
    setExpanded(prev => 
      prev.includes(nodeName) 
        ? prev.filter(name => name !== nodeName)
        : [...prev, nodeName]
    );
  };

  const renderNode = (currentNode: TreeNode) => {
    const isExpanded = expanded.includes(currentNode.name);

    return (
      <Box key={currentNode.name} mb={0.5}>
        {currentNode.children.length > 0 || currentNode.size > 0 ? (
          <Accordion 
            expanded={isExpanded} 
            onChange={() => handleExpand(currentNode.name)}
            sx={{ backgroundColor: 'background.paper' }}
          >
            <AccordionSummary expandIcon={currentNode.size > 0 ? <ExpandMoreIcon fontSize="small" /> : null}>
              <Box display="flex" alignItems="center" width="100%">
                <IconButton size="small">
                  {currentNode.children.length > 0 ? <FolderIcon fontSize="small" color="primary" /> : <ImageIcon fontSize="small" color="secondary" />}
                </IconButton>
                <Typography variant="body2" sx={{ flexGrow: 1, ml: 1 }}>
                  {currentNode.name}
                </Typography>
                <Chip label={`Size: ${currentNode.size}`} size="small" sx={{ ml: 2, height: 20, fontSize: '0.7rem' }} />
              </Box>
            </AccordionSummary>
            <AccordionDetails>
              {isExpanded && currentNode.children.length > 0 && (
                <Box pl={2}>
                  {currentNode.children.map(childNode => renderNode(childNode))}
                </Box>
              )}
            </AccordionDetails>
          </Accordion>
        ) : (
          <Box display="flex" alignItems="center" width="100%">
            <IconButton size="small">
              <ImageIcon fontSize="small" color="secondary" />
            </IconButton>
            <Typography variant="body2" sx={{ flexGrow: 1, ml: 1 }}>
              {currentNode.name}
            </Typography>
            <Chip label={`Size: ${currentNode.size}`} size="small" sx={{ ml: 2, height: 20, fontSize: '0.7rem' }} />
          </Box>
        )}
      </Box>
    );
  };

  return (
    <Box>
      {renderNode(node)}
    </Box>
  );
};

export default TreeView;